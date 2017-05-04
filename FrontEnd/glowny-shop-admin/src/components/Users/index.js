import React from 'react'
import { connect } from 'react-redux'
import { getGraph } from '../../actions/graphql.js'

import UserList from './UserList'

class Users extends React.Component {
  componentDidMount = () => {
    this.props.dispatch(getGraph(`
      viewer {  
        adminUsers { 
          id 
          firstName 
          lastName 
          email 
          isActive 
          adminRoles {
            id
            name
          }
        }
      }`)
    );
  }

  render() {
    //let dispatch = this.props.dispatch
    //let fetchInProgress = String(this.props.store.get('fetching'));
    //let queryText;
    //let viewer = this.props.store.get('data').toObject()
    //console.log(this.props.store.data)
    const { adminUsers } = this.props.store.data
    //console.log(adminUsers)
    return (
      <UserList adminUsers={adminUsers} />
    )
  }
}

const mapStateToProps = (state) => ({
  store: state.queryReducer,
})

const usersRedux = connect(
  mapStateToProps,
)(Users)

export default usersRedux

