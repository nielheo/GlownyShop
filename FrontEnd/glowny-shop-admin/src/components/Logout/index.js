import React from 'react'
import { setUserToken } from '../Common/Cookies'

export default class AboutUs extends React.Component {
  componentDidMount = () => {
    setUserToken('')
    this.props.history.push('/')
  }
  render() {
    return (
      <div>
        Logging out ...
      </div>
    )
  }
}
