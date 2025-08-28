import type { ReviewData } from "../types/ReviewData";

export function createReview(
  name: string,
  comment: string,
  ratings: Record<string, number>
): ReviewData {
  const values = Object.values(ratings);
  const average =
    values.reduce((total, value) => total + value, 0) / values.length;

  return {
    guid: crypto.randomUUID(),
    name,
    comment,
    rating: Number(average.toFixed(1)),
  };
}