import { ClipLoader } from 'react-spinners'

const override = {
    display: 'block',
    margin: '100px auto'
}

function Spinner({loading}) {
  return (
    <ClipLoader
        loading={loading}
        color='#4338ca'
        cssOverride={override}
        size={150}
    />
  )
}

export default Spinner