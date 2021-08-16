import { TabType } from '../enums/TabType';

export interface ITab {
  id: string;
  type: TabType;
  textValue: string;
}
