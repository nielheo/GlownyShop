import React from 'react'
import { connect } from 'react-redux'
import { getGraph } from '../../actions/graphql.js'

class Users extends React.Component {
  componentDidMount = () => {
    this.props.dispatch(getGraph(`{ 'query': 'query { viewer { adminUsers { id firstName lastName email isActive }}}' }`));
  }
  render() {
    //let dispatch = this.props.dispatch
    //let fetchInProgress = String(this.props.store.get('fetching'));
    //let queryText;
    //let viewer = this.props.store.get('data').toObject()
    console.log(this.props.store.data)
    return (
      <div>
        User Management
      </div>
    );
  }
}

const mapStateToProps = (state) => ({
  store: state.queryReducer,
})

const usersRedux = connect(
  mapStateToProps,
)(Users)

export default usersRedux

