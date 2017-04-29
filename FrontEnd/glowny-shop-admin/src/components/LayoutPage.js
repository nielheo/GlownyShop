'use strict';

import React from 'react';
import { Link } from 'react-router-dom';
import Header from './Header'

const styles = {
  container: {
    padding: 15,
  },
};

export default class Layout extends React.Component {
  render() {
    return (
      <div>
        <Header />
        <div style={styles.container}>{this.props.children}</div>
      </div>
    );
  }
}
