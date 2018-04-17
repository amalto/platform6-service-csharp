import * as React from 'react'
import * as Platform6 from '@amalto/platform6-ui'

declare namespace ServiceConfiguration {
    interface Props extends ServiceConfiguration, Platform6.DynamicComponent.CustomProps {}

    interface State {}
}

class ServiceConfiguration extends React.Component<ServiceConfiguration.Props, ServiceConfiguration.State> {
    render () {
        return <div>
            <h4 className="mgb-15 mgt-15">Hello ! I am a service developed in C#.</h4>
        </div>
    }
}

export default Platform6.reactRedux.connect()(ServiceConfiguration)