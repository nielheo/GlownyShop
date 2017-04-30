import React from 'react'
import {grey900} from 'material-ui/styles/colors'

const styles = {
  container: {
    textAlign: 'left',
    width: 80,
    backgroundColor: grey900,
    color: '#FFFFFF',
    padding:10,
    fontSize: '1.2em',
  }
}

const PaperHeader = ({title}) => {
  return(
    <div style={styles.container}>{title}</div>
  )
}

export default PaperHeader