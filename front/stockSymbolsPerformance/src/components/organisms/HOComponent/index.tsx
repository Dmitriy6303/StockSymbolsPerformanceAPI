import React from 'react';
import { ContentPanel, TabsPanel } from '../../molecules';
import { SnackBar } from '../../atoms';
import { ITab } from '../../../common/models';

import './styles.css';

interface IState {
  selectedTab: ITab | null;
}

class HOComponent extends React.PureComponent<{}, IState> {
  constructor(props: {}) {
    super(props);

    this.state = {
      selectedTab: null,
    }
  }

  render() {
    const { selectedTab } = this.state;

    return (
      <div className="app-content">
        <TabsPanel
          selectedTab={selectedTab}
          setSelectedTab={(tab: ITab) => this.setState({ selectedTab: tab })}
        />
        <ContentPanel selectedTab={selectedTab} />
        <SnackBar />
      </div>
    );
  }
}

export default HOComponent;
