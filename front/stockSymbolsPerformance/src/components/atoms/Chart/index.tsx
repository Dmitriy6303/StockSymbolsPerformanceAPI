/* eslint-disable no-template-curly-in-string */
import React from 'react';
import moment from 'moment';
import Highcharts from 'highcharts';
import HighchartsReact from 'highcharts-react-official';
import { IChart } from '../../../common/models';

interface IProps {
  data: IChart;
  symbol: string;
}

export const Chart: React.FC<IProps> = ({ data, symbol }: IProps) => {
  const prepareLabels = () => {
    const labels = 
      data.performanceOfIncomingSymbol.length > data.performanceOfDefaultSymbol.length
        ? data.performanceOfIncomingSymbol.map((item) => moment(item.time).format('DD/MM/YYYY HH:mm'))
        : data.performanceOfDefaultSymbol.map((item) => moment(item.time).format('DD/MM/YYYY HH:mm'))
    return labels;
  };

  const prepareData = (isGiven: boolean) => {
    const values = isGiven
      ? data.performanceOfIncomingSymbol.map((item) => ({ name: item.price, y: item.performance }))
      : data.performanceOfDefaultSymbol.map((item) => ({ name: item.price, y: item.performance }));
    return values;
  };

  const options = {
    title: {
      text: 'Performance'
    },
    chart: {
      height: 700,
    },
    series: [
      {
        name: `${symbol} performance`,
        data: prepareData(true),
        color: '#2f7ed8',
        tooltip: {
          headerFormat: '{point.x} <br>',
          pointFormat:
            "Performance: {point.y}% <br> <b>Price: {point.name}</b>"
        },
      },
      {
        name: 'SPY performance',
        data: prepareData(false),
        color: '#a6c96a',
        tooltip: {
          headerFormat: '{point.x} <br>',
          pointFormat:
            "Performance: {point.y}% <br> <b>Price: {point.name}</b>"
        },
      },
    ],
    xAxis: {
      categories: prepareLabels(),
    },
    yAxis: {
      labels: {
        format: '{value}%',
      },
    }
  }

  return (
    <HighchartsReact
      highcharts={Highcharts}
      options={options}
    />
  );
};

