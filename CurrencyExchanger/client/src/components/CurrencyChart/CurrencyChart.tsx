import React, { useState, useMemo } from 'react';
import styles from './CurrencyChart.module.css';
import {
  Chart as ChartJS,
  LineElement,
  CategoryScale,
  LinearScale,
  PointElement,
  Filler,
  ChartOptions,
  Tooltip
} from 'chart.js';
import { Line } from 'react-chartjs-2';
import { usePrices } from '../../hooks/usePrices.ts';

ChartJS.register(LineElement, CategoryScale, LinearScale, PointElement, Filler, Tooltip);

const intervals = ['1 min', '2 min', '3 min', '4 min', '5 min'] as const;

type Interval = (typeof intervals)[number];

const intervalToMs: Record<Interval, number> = {
  '1 min': 60 * 1000,
  '2 min': 2 * 60 * 1000,
  '3 min': 3 * 60 * 1000,
  '4 min': 4 * 60 * 1000,
  '5 min': 5 * 60 * 1000
};

const getFullDateTime = (dateTime: string) => {
  const date = new Date(dateTime);
  const days = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  const dayOfWeek = days[date.getUTCDay()];

  return `${dayOfWeek}, ${date.toLocaleString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
    second: '2-digit',
    hour12: false,
    timeZone: 'UTC'
  })} UTC`;
};

const formatPrice = (price: number) => {
  return `${price.toFixed(2)}`;
};

export const CurrencyChart = React.memo(() => {
  const [selectedInterval, setSelectedInterval] = useState<Interval>('5 min');
  const { prices } = usePrices();

  const filteredPrices = useMemo(() => {
    if (!prices || prices.length === 0) return [];

    const now = Date.now();
    const intervalMs = intervalToMs[selectedInterval];
    const threshold = now - intervalMs;

    return prices.filter((p) => {
      const pTime = new Date(p.dateTime).getTime();
      return pTime >= threshold;
    });
  }, [prices, selectedInterval]);

  const chartData = useMemo(() => ({
    labels: filteredPrices.map((p) => getFullDateTime(p.dateTime)),
    datasets: [
      {
        data: filteredPrices.map((p) => p.price),
        borderColor: 'rgb(75,112,192)',
        backgroundColor: 'rgba(75,126,192,0.2)',
        fill: true,
        tension: 0.1,
        pointBackgroundColor: 'rgb(75,81,192)',
        pointBorderColor: '#0b39e8',
        pointBorderWidth: 1,
        pointRadius: 3,
        pointHoverRadius: 6,
        pointHoverBackgroundColor: '#0b39e8',
        pointHoverBorderColor: '#ffffff',
        pointHoverBorderWidth: 2,
      }
    ]
  }), [filteredPrices]);

  const options = useMemo((): ChartOptions<'line'> => ({
    responsive: false,
    plugins: {
      legend: {
        display: false
      },
      tooltip: {
        enabled: true,
        backgroundColor: 'rgba(255,255,255,0.8)',
        titleColor: 'rgba(126,117,117,0.51)',
        bodyColor: 'rgba(126,117,117, 1)',
        titleFont: {
          size: 12,
          weight: 'bold'
        },
        bodyFont: {
          size: 14,
          weight: 'bold'
        },
        padding: 12,
        cornerRadius: 6,
        displayColors: false,
        callbacks: {
          title: () => '',
          label: (context) => {
            const index = context.dataIndex;
            const priceData = filteredPrices[index];

            if (!priceData) return '';

            return [
              `${getFullDateTime(priceData.dateTime)}`,
              `${formatPrice(priceData.price)}`
            ];
          }
        }
      }
    },
    scales: {
      x: {
        display: false,
        grid: {
          color: 'rgba(0, 0, 0, 0.1)'
        }
      },
      y: {
        grid: {
          color: 'rgba(0, 0, 0, 0.1)'
        },
        beginAtZero: true,
        ticks: {
          callback: (value) => formatPrice(Number(value))
        }
      }
    },
    interaction: {
      intersect: false,
      mode: 'index'
    },
    hover: {
      intersect: false
    }
  }), [filteredPrices]);

  return (
    <div className={styles.chartContainer}>
      <div className={styles.intervalSelector}>
        {intervals.map((interval) => (
          <button
            key={interval}
            className={`${styles.intervalButton} ${selectedInterval === interval ? styles.active : ''}`}
            onClick={() => setSelectedInterval(interval)}
          >
            {interval}
          </button>
        )).reverse()}
      </div>

      <div className={styles.chartWrapper}>
        <Line
          key={`${selectedInterval}-${filteredPrices.length}`}
          data={chartData}
          options={options}
          width={400}
          height={200}
        />
      </div>
    </div>
  );
});