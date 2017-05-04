import React from 'react'
import { connect } from 'react-redux'
import { getGraph } from '../../actions/graphql.js'

import {
  Table,
  TableBody,
  TableHeader,
  TableHeaderColumn,
  TableRow,
  TableRowColumn,
} from 'material-ui/Table'
import Toggle from 'material-ui/Toggle'

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
      }`));
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
      <Table fixedHeader={true} selectable={false}>
        <TableHeader displaySelectAll={false}
            adjustForCheckbox={false}>
          <TableRow>
            <TableHeaderColumn>First Name</TableHeaderColumn>
            <TableHeaderColumn>Last Name</TableHeaderColumn>
            <TableHeaderColumn>Email</TableHeaderColumn>
            <TableHeaderColumn>Role(s)</TableHeaderColumn>
            <TableHeaderColumn>Active</TableHeaderColumn>
          </TableRow>
        </TableHeader>
        <TableBody displayRowCheckbox={false}>
          {
            adminUsers && adminUsers.map(user => 
              <TableRow key={user.id}>
                <TableRowColumn>{user.firstName}</TableRowColumn>
                <TableRowColumn>{user.lastName}</TableRowColumn>
                <TableRowColumn>{user.email}</TableRowColumn>
                <TableRowColumn>{user.adminRoles.map(role => role.name).join('; ')}</TableRowColumn>
                <TableRowColumn><Toggle defaultToggled={user.isActive}/></TableRowColumn>
              </TableRow>)
          }
          
        </TableBody>
      </Table>
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

