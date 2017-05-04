import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import * as types from '../../actions/actionTypes.js'
import { withRouter } from 'react-router'
import {
  Link
} from 'react-router-dom'
import PropTypes from 'prop-types'
import AppBar from 'material-ui/AppBar'
import LinearProgress from 'material-ui/LinearProgress'
import {cyan500} from 'material-ui/styles/colors'



const styles = {
  title: {
    cursor: 'pointer',
    color: '#FFFFFF',
    textDecoration: 'none',
  },
  appBar: {
    backgroundColor: cyan500,
  }
};

class Header extends React.Component {
  static propTypes = {
    match: PropTypes.object.isRequired,
    location: PropTypes.object.isRequired,
    history: PropTypes.object.isRequired
  }
  render() {
    const { location } = this.props
    return (
      <section>
      { location.pathname !== '/login' && location.pathname !== '/404' &&
      <AppBar
        style={styles.appBar}
        title={<Link to='/' style={styles.title}>Thrive People</Link>}
        iconClassNameRight="muidocs-icon-navigation-expand-more"
        onLeftIconButtonTouchTap={this.props.onNavigationClick}
      /> }
      { this.props.onProgress && <LinearProgress mode="indeterminate" />
      }
      </section>
    )
  }
}

const _updateHomeOnProgressAction = (onProgress) => ({
  type: types.UPDATE_HOME_ON_PROGRESS,
  onProgress: onProgress,
})

const dispatchToProps = (dispatch) => ({
  _updateHomeOnProgressAction: bindActionCreators(_updateHomeOnProgressAction, dispatch),
})

const stateToProps = (state) => ({
  onProgress: state.homeReducer.onProgress,
})

const headerRedux = connect(
  stateToProps,
  dispatchToProps,
)(Header);

export default withRouter(headerRedux)