'use strict';
import React from 'react'
import Paper from 'material-ui/Paper'
import {yellow400} from 'material-ui/styles/colors'
import AppBar from 'material-ui/AppBar'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'
import { setUserToken } from '../Common/Cookies'


const styles = {
  container: {
    textAlign: 'left',
    paddingTop: 100,
    maxWidth: 400,
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
  constructor(props) {
    super(props)
    this.state = {
      emailError: '',
      passwordError: '',
      email: '',
      password: '',
      buttonClick: false,
    }
  }

  _loginButtonClick = async () => {
    this.setState({buttonClick: true})
    if (this.state.email !== '' && this.state.password !== '') {
      let response
      try {
        response = await fetch('http://localhost:5555/connect/token', {
          method: 'POST',
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded',
          },
          body: 'grant_type=password&username=superadmin@glowny-shop.com&password=P@ssw0rd', 
          
        })
        .then((response) => response.json())
      } catch (err) {
        //this._changeLoadingStatus(false);
        response = null;
      }

      if(response.access_token) {
        setUserToken(response.access_token)
        this.props.history.push('/')
      }
      //console.log(response.access_token)

      //setUserToken(this.state.email)
      //this.props.history.push('/')
    }
  }

  _updateField(name, value) {
    this.setState({[name]: value.target.value})
  }

  render() {
    return (
      <div style={styles.container}>
        <div style={styles.header} >Login</div>
        <TextField
          hintText='email'
          fullWidth={true}
          floatingLabelText='Email'
          errorText={this.state.buttonClick && this.state.email.length === 0 && 'Enter an email' }
          value={this.state.email}
          onChange={this._updateField.bind(this, 'email')}
        />
        <TextField
          hintText='password'
          fullWidth={true}
          floatingLabelText='Password'
          errorText={this.state.buttonClick && this.state.password.length === 0 && 'Enter a password' }
          onChange={this._updateField.bind(this, 'password')}
          value={this.state.password}
          type='password'
        />
        <RaisedButton 
          label='Login'
          fullWidth={true} 
          secondary={true} 
          style={styles.button}
          onClick={this._loginButtonClick} />
      </div>

    )
  }
}
