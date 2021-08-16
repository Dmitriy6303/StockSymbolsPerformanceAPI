import { ISymbolList } from './ISymbolList';

export interface IChart {
  performanceOfIncomingSymbol: ISymbolList[],
  performanceOfDefaultSymbol: ISymbolList[],
}
