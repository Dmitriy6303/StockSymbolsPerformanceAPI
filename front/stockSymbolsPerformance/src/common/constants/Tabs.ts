import { TabType } from '../enums/TabType';
import { ITab } from '../models';

export const tabs: ITab[] = [
  {
    id: '1',
    textValue: 'Performance by day',
    type: TabType.PERFORMANCE_DAY,
  },
  {
    id: '2',
    textValue: 'Performance by hour',
    type: TabType.PERFORMANCE_HOUR,
  },
];
