'use strict';

import React from 'react';
import { Link } from 'react-router-dom';
import Drawer from 'material-ui/Drawer';
import Menu from 'material-ui/Menu';
import MenuItem from 'material-ui/MenuItem';
import AppBar from 'material-ui/AppBar';
import Header from './Header'

const styles = {
  container: {
    padding: 15,
  },
  drawer: {
    backgroundColor: '#FFD600',
  },
};

export default class LayoutPage extends React.Component {
  constructor(props) {
    super(props)
    this.state = {open: false}
  }

  _navigationClickHandler = () => this.setState({open: !this.state.open})
  _handleClose = () => this.setState({open: false})

  render() {
    return (
      <div>
        <Header onNavigationClick={this._navigationClickHandler} />
        <Drawer
          docked={false}
          width={250}
          open={this.state.open}
          containerStyle={styles.drawer}
          onRequestChange={(open) => this.setState({open})}
        >
          <AppBar iconElementLeft={<span></span>} title='Thrive People' />
          <Menu>
            <MenuItem href='/' primaryText='Home' />
            <MenuItem href='/aboutus' primaryText='About Us' />
            <MenuItem href='/login' primaryText='Login' />
          </Menu>
        </Drawer>
        <div style={styles.container}>{this.props.children}</div>
      </div>
    );
  }
}
