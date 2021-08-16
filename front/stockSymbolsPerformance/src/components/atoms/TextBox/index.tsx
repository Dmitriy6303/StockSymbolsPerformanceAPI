import React from 'react';

import './styles.css';

interface IProps {
  onChange: (value: string) => void;
}

export default class TextBox extends React.PureComponent<IProps> {
  render() {
    const { onChange } = this.props;

    return (
      <input
        className="textbox"
        onChange={(e) => {
          const { target } = e;
          if (e?.target?.value && e.target.value.length > 1) onChange(target.value);
        }}
      />
    );
  }
};

