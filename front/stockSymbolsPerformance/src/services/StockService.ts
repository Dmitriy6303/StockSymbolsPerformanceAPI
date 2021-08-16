import { axiosInstance } from '../utils';
import { IChart } from '../common/models';

export class StockService {
  public static async getPerformanceByDay(symbol: string): Promise<IChart> {
    const result = await axiosInstance.get<IChart>(
      `api/StockSymbols/${symbol}/perfCompByDay`)
      .then((result) => result.data);

    return result;
  }

  public static async getPerformanceByHour(symbol: string): Promise<IChart> {
    const result = await axiosInstance.get<IChart>(
      `api/StockSymbols/${symbol}/perfCompByHour`)
      .then((result) => result.data);

    return result;
  }

  public static async updateSPYData(): Promise<boolean> {
    const result = await axiosInstance.get(
      'api/StockSymbols/spy/update')
      .then(() => true).catch(() => false);

    return result;
  }
}
