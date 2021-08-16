import React from 'react';

import './styles.css';

interface IProps {
  onClick: () => void;
  text: string;
}

export default class Button extends React.PureComponent<IProps> {
  render() {
    const { onClick, text } = this.props;

    return (
      <button
        className="button"
        onClick={onClick}
      >
        {text}
      </button>
    );
  }
};

