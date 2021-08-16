import React from 'react';
import { Chart, TextBox, Button, Loader } from '../../atoms';
import { IChart } from '../../../common/models';
import { TabType } from '../../../common/enums/TabType';
import { ITab } from '../../../common/models';
import { StockService } from '../../../services/StockService';

import './styles.css';

interface IProps {
  selectedTab: ITab | null;
}

interface IState {
  dataChart?: IChart;
  textValue: string;
  timer?: NodeJS.Timeout;
  isLoading: boolean;
}

export default class ContentPanel extends React.PureComponent<IProps, IState>{
  constructor(props: IProps) {
    super(props)

    this.state = {
      textValue: '',
      isLoading: false,
    };

    this.loadData = this.loadData.bind(this);
    this.updateSPYData = this.updateSPYData.bind(this);
  }

  private loadData = async () => {
    const { selectedTab } = this.props;
    const { textValue } = this.state;
    if (selectedTab) {
      this.setState({ isLoading: true });
      let result;
      switch (selectedTab.type) {
        case TabType.PERFORMANCE_DAY:
          result = await StockService.getPerformanceByDay(textValue);
          break;
        case TabType.PERFORMANCE_HOUR:
          result = await StockService.getPerformanceByHour(textValue);
          break;
        default:
          break;
      }
      this.setState({ dataChart: result, isLoading: false });
    } else {
      var snackBarElement = document.getElementById("snackbar");
      if (snackBarElement) {
        snackBarElement.className = "show";
        setTimeout(() => { if (snackBarElement) snackBarElement.className = snackBarElement.className.replace("show", ""); }, 2900);
      }
    }
  }

  private updateSPYData = async () => {
    await StockService.updateSPYData();
  }

  render() {
    const { dataChart, textValue, isLoading } = this.state;

    return (
      <div className="content-panel-container">
        <div className="action-group-panel">
          <TextBox onChange={(text) => this.setState({ textValue: text })} />
          <Button onClick={this.loadData} text="Get stock symbol data" />
          <Button onClick={this.updateSPYData} text="Update SPY symbol data" />
        </div>
        {
          dataChart && (
            <Chart data={dataChart} symbol={textValue} />
          )
        }
        {
          isLoading && 
          <div className="loader-container">
            <Loader />
          </div>
        }
      </div>
    );
  }
}
