import { useState, FC } from 'react';
import { Accordion, CircularProgress } from '@mui/material';
import { Link, LinkOff } from '@mui/icons-material';
import { useAppDispatch } from '../../redux/hooks';
import { reloadService, updateService, deleteService, getService, setTimeoutService } from '../ServiceList/ServiceListStore';
import { ServiceInfo } from '../ServiceList/data';
import { AccordionBody, AccordionHeader, Text } from '../../components';
import { Parameters } from '../ParameterList/data';
import ParameterList from '../ParameterList/ParameterList';
import './Service.scss';

interface ServiceProps {
    service: ServiceInfo;
}

const Service: FC<Readonly<ServiceProps>> = ({ service }) => {
    const dispatch = useAppDispatch();
    const [expanded, setExpanded] = useState(false);
    const [modifiedParameters, setModifiedParameters] = useState(service.parameters);

    const timeoutService = () => {
        const timeoutId = setTimeout(() => dispatch(getService(service)), 10000);
        dispatch(setTimeoutService({
            name: service.name,
            timeoutId: timeoutId
        }));
    };
    
    const onSaveClick = async (parameters: Parameters) => {
        const result = await dispatch(updateService({
            name: service.name,
            parameters: parameters
        }));
        if (result.meta.requestStatus === 'fulfilled')
            timeoutService();
    };

    const onReloadClick = async () => {
        const result = await dispatch(reloadService(service));
        if (result.meta.requestStatus === 'fulfilled')
            timeoutService();
    };

    const onDeleteClick = async () => {
        const result = await dispatch(deleteService(service));
        if (result.meta.requestStatus === 'fulfilled')
            timeoutService();
    };
    
    return (
        <Accordion className={ !service.state ? "service-opacity" : "" } 
            expanded={expanded} 
            onChange={(_e, s) => setExpanded(s)}
        >
            <AccordionHeader>
                <div className="service-header-container">
                    {
                        service.loading
                            ? <CircularProgress className="service-progress" />
                            : (service.state?.isActive
                                ? <Link className="service-success-icon" />
                                : <LinkOff className="service-fail-icon" />)
                    }
                    <div>
                        <div className="service-header-container service-name">
                            <Text>{service.name}</Text>
                        </div>
                        {
                            service.updateError &&
                                <Text assignment="error" multiline>{ service.updateError }</Text>
                        }
                    </div>
                </div>
            </AccordionHeader>
            <AccordionBody indent>
                <ParameterList
                    parameters={service.parameters}
                    modifiedParameters={modifiedParameters}
                    disable={service.loading}
                    error={service.state?.error}
                    stackTrace={service.state?.stackTrace}
                    setModifiedParameters={setModifiedParameters}
                    onSave={onSaveClick}
                    onReload={onReloadClick}
                    onDelete={onDeleteClick}
                />
            </AccordionBody>
        </Accordion>
    );
};

export default Service;