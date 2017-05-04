import React from 'react'
import UserItem from './UserItem'
import Snackbar from 'material-ui/Snackbar'

import {
  Table,
  TableBody,
  TableHeader,
  TableHeaderColumn,
  TableRow,
} from 'material-ui/Table'

class UserList extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      snackbarOpen: false,
      snackbarMessage: '',
    }
  }

  _handleRequestClose = () => {
    this.setState({ snackbarOpen: false })
  }

  _handleSetSnackbar = (open, message) => {
    this.setState({
      snackbarOpen: open,
      snackbarMessage: message,
    })
  }

  render()  {
    const { adminUsers } = this.props
    return (
      <div>
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
                <UserItem user={user} key={user.id} setSnackbar={this._handleSetSnackbar} /> )
            }
          </TableBody>
        </Table>
        <Snackbar
            open={this.state.snackbarOpen}
            message={this.state.snackbarMessage}
            autoHideDuration={4000}
            onRequestClose={this._handleRequestClose}
          />
      </div>
    )
  }
}

export default UserList