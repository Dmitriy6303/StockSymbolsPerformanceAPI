import React from 'react';
import './styles.css';

interface IProps {
  isSelected: boolean;
  text: string;
  id: string;
  onClick: () => void;
}

export default class Tab extends React.PureComponent<IProps> {
  render() {
    const { onClick, isSelected, text, id } = this.props;
    return (
      <div
        key={id}
        id={id}
        className={`tab ${isSelected ? 'is-selected' : ''}`}
        onClick={onClick}
      >
        <span>{text}</span>
      </div>
    );
  }
}
