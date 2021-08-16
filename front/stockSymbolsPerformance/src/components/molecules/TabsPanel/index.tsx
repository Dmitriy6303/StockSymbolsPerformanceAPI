import React from 'react';
import { ITab } from '../../../common/models';
import { tabs } from '../../../common/constants/Tabs';
import Tab from '../../atoms/Tab';

import './styles.css';

interface IProps {
  setSelectedTab: (id: ITab) => void;
  selectedTab: ITab | null;
}

export default class TabsPanel extends React.PureComponent<IProps> {
  private tabRender = (tab: ITab) => {
    const { selectedTab, setSelectedTab } = this.props;
    return (
      <Tab
        isSelected={!!selectedTab && selectedTab.id === tab.id}
        onClick={() => setSelectedTab(tab)}
        id={tab.id}
        key={tab.id}
        text={tab.textValue}
      />
      );
  };

  render() {
    
    return (
      <div className="tabs-container">
        {
          tabs && tabs.map((tab) => (
            this.tabRender(tab)
          ))
        }
      </div>
    );
  }
}
