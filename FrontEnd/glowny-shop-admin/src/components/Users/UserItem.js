import React from 'react'

import {
  TableRow,
  TableRowColumn,
} from 'material-ui/Table'
import Toggle from 'material-ui/Toggle'

class UserItem extends React.Component 
{ 
  _isActiveClickHandler = (value, e) => {
    console.log(value)
    console.log(e.target.checked)
    this.props.setSnackbar(true, value + ' is ' + (e.target.checked ? 'checked' : 'not checked'))
  }
  
  render() {
    const { user } = this.props
    return(
        <TableRow>
          <TableRowColumn>{user.firstName}</TableRowColumn>
          <TableRowColumn>{user.lastName}</TableRowColumn>
          <TableRowColumn>{user.email}</TableRowColumn>
          <TableRowColumn>{user.adminRoles.map(role => role.name).join('; ')}</TableRowColumn>
          <TableRowColumn>
            <Toggle defaultToggled={user.isActive} 
                onClick={this._isActiveClickHandler.bind(this, user.id)}/>
          </TableRowColumn>
        </TableRow>
    )
  }

}

export default UserItem