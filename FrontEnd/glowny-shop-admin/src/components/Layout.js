import React from 'react'
import {cyan500} from 'material-ui/styles/colors'
import Drawer from 'material-ui/Drawer'
import Menu from 'material-ui/Menu'
import MenuItem from 'material-ui/MenuItem'
import AppBar from 'material-ui/AppBar'
import Header from './Header'
import { getUserToken } from '../components/Common/Cookies'

const styles = {
  container: {
    padding: 15,
  },
  drawer: {
    backgroundColor: '#FFFFFF',
  },
  AppBar: {
    backgroundColor: cyan500,
  }
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
          <AppBar iconElementLeft={<span></span>} title='Thrive People' style={styles.AppBar} />
          <Menu>
            <MenuItem href='/' primaryText='Home' />
            <MenuItem href='/users' primaryText='Users' />
            { getUserToken() 
              ? <MenuItem href='/logout' primaryText='Log Out' />
              : <MenuItem href='/login' primaryText='Login' />
            }
          </Menu>
        </Drawer>
        <div style={styles.container}>{this.props.children}</div>
      </div>
    );
  }
}
