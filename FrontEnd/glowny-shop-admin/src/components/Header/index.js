import React from 'react'
import {
  Link
} from 'react-router-dom'
import PropTypes from 'prop-types'
import AppBar from 'material-ui/AppBar'
import { withRouter } from 'react-router'

const styles = {
  title: {
    cursor: 'pointer',
    color: '#FFFFFF',
    textDecoration: 'none',
  },
};

class Layout extends React.Component {
  static propTypes = {
    match: PropTypes.object.isRequired,
    location: PropTypes.object.isRequired,
    history: PropTypes.object.isRequired
  }
  render() {
    const { match, location, history } = this.props
    return (
      location.pathname !== '/login' && location.pathname !== '/404' &&
      <AppBar
        title={<Link to='/' style={styles.title}>Thrive People</Link>}
        iconClassNameRight="muidocs-icon-navigation-expand-more"
        onLeftIconButtonTouchTap={this.props.onNavigationClick}
      />
    )
  }
}

export default withRouter(Layout)