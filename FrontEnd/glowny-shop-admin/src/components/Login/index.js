'use strict';
import React from 'react'
import Paper from 'material-ui/Paper'
import {yellow400} from 'material-ui/styles/colors'
import AppBar from 'material-ui/AppBar'
import PaperHeader from '../Common/PaperHeader'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'

const styles = {
  container: {
    textAlign: 'left',
    paddingTop: 150,
    width: 300,
    margin: 'auto'
  },
  header: {
    fontSize: '1.3em',
    fontWeight: '600',
  },
  button: {
    marginTop: 12,
    float: 'right',
  }
}


export default class IndexPage extends React.Component {
  render() {
    return (
      <div style={styles.container}>
        <div style={styles.header} >Login</div>
        <TextField
          hintText='email'
          fullWidth='true'
          floatingLabelText="Email"
        />
        <TextField
          hintText='password'
          fullWidth='true'
          floatingLabelText="Password"
          type="password"
        />
        <RaisedButton label="Login" primary={true} style={styles.button} />
      </div>

    )
  }
}
