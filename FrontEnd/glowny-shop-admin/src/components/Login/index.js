'use strict';
import React from 'react'
import { connect } from 'react-redux'
import { bindActionCreators } from 'redux'
import * as types from '../../actions/actionTypes.js'
import Paper from 'material-ui/Paper'
import {yellow400} from 'material-ui/styles/colors'
import AppBar from 'material-ui/AppBar'
import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton'
import { setUserToken } from '../Common/Cookies'
import debounce from 'lodash.debounce'

const styles = {
  container: {
    textAlign: 'left',
    paddingTop: 100,
    maxWidth: 400,
    margin: 'auto'
  },
  header: {
    fontSize: '2em',
    fontWeight: '600',
  },
  button: {
    marginTop: 12,
    float: 'right',
  }
}


class Login extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      emailError: '',
      passwordError: '',
      email: '',
      password: '',
      buttonClick: false,
    }
    this._login = debounce(this._login, 1500)
  }

  _loginButtonClick = () => {
    this.setState({ buttonClick: true })
    if (this.state.email !== '' && this.state.password !== '') {
      this.props._updateHomeOnProgressAction(true)
      this._login()
    } 
  }

  _login = async () => {
    
      
    //this.setState({ buttonClick: true })
    let response
    try {
      response = await fetch('http://localhost:5555/connect/token', {
        method: 'POST',
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: 'grant_type=password&username=' + this.state.email + '&password=' + this.state.password, 
        
      })
      .then((response) => {
        console.log(response)
        this.props._updateHomeOnProgressAction(false)
        if (response.status >= 400 && response.status < 600) {
          this.setState({
            emailError: ' ',
            passwordError: 'Invalid login. Try again.',
          })
          throw new Error("Bad response from server");
        }
        return response.json()
      })
      .then((response) => {
        if(response.access_token) {
          setUserToken(response.access_token)
          this.props.history.push('/')
        }
      })
    } 
    catch (err) {
      this.props._updateHomeOnProgressAction(false)
      console.log(err)
      //this._changeLoadingStatus(false);
      response = null;
    }
  }

  _updateField(name, value) {
    this.setState({
      [name]: value.target.value,
      emailError: '',
      passwordError: '',
    })
  }

  render() {
    return (
      <div style={styles.container}>
        
        <div style={styles.header} >Login</div>
        <TextField
          hintText='email'
          fullWidth={true}
          floatingLabelText='Email'
          errorText={ (this.state.buttonClick && this.state.email.length === 0 && 'Enter an email')
            || this.state.emailError }
          value={this.state.email}
          onChange={this._updateField.bind(this, 'email')}
        />
        <TextField
          hintText='password'
          fullWidth={true}
          floatingLabelText='Password'
          errorText={ (this.state.buttonClick && this.state.password.length === 0 && 'Enter a password')
            || this.state.passwordError }
          onChange={this._updateField.bind(this, 'password')}
          value={this.state.password}
          type='password'
        />
        <RaisedButton 
          label='Login'
          fullWidth={true} 
          primary={true} 
          style={styles.button}
          onClick={this._loginButtonClick} />
      </div>

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

const loginRedux = connect(
  stateToProps,
  dispatchToProps,
)(Login);

export default loginRedux
