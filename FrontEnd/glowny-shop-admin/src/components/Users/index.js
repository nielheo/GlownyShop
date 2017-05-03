'use strict'
import React from 'react'
import Relay from 'react-relay'

class Users extends React.Component {
  render() {
    console.log(this.props.viewer)
    return (
      <div>
        User Management
      </div>
    );
  }
}



export default Relay.createContainer(Users, {
  fragments: {
    viewer: () => Relay.QL`
      fragment on Viewer {
        adminUsers {
          id
          firstName
          lastName
          email
          isActive
        }
      }
    `,
  },
})

