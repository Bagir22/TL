import { CurrencyExchangerView } from "./CurrencyExchanger.view";
import { useCurrencyExchangerState } from "./CurrencyExchanger.state";
import { Loader } from "../Loader/Loader";
import { ErrorMessage } from "../ErrorMessage/ErrorMessage";

const CurrencyExchanger = () => {
  const { loading, error } = useCurrencyExchangerState();

   if (loading) {
    return <Loader />;
  }

  if (error) {
    return (
      <ErrorMessage
        message={error}
      />
    );
  }

  return <CurrencyExchangerView />;
};

export default CurrencyExchanger;