import { FC, useEffect } from 'react';
import { DisplaySettings } from '@mui/icons-material';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import { RootState } from '../../redux/store';
import { update, removeService, setError, updateServiceRange, setLoading, authorized } from './ServiceListStore';
import { useConnection } from '../../hooks';
import { api } from '../../utils/api';
import { unauthorized } from '../../App/AppStore';
import Service from '../Service/Service';
import NavBar from '../NavBar/NavBar';
import Error from '../../components/Error/Error';
import './ServiceList.scss';

const ServiceList: FC = () => {
    const dispatch = useAppDispatch();
    const serviceList = useAppSelector(({ serviceList }: RootState) => serviceList);

    const connection = useConnection('/hub/services', {
        invoke: () => {
            dispatch(setLoading(true));
            return ['Services', []];
        },
        callback: (running, authError, message) => {
            if (authError) {
                dispatch(unauthorized());
            } else {
                dispatch(setLoading(!running));
                dispatch(setError(message));
            }
        },
        auth: api.refresh,
        handlers: [
            ['Service', service => dispatch(update(service))],
            ['Services', services => dispatch(updateServiceRange(services))],
            ['RemoveService', service => dispatch(removeService(service))]
        ]
    });

    useEffect(() => {
        connection.start();
        return () => {
            connection.stop();
        };
    }, []);

    useEffect(() => {
        if (!serviceList.auth) {
            dispatch(authorized());
            dispatch(unauthorized());
        }
    }, [serviceList.auth]);

    return (
        <NavBar
            title={`Services (${serviceList.activeServiceCount}/${serviceList.serviceCount})`}
            icon={<DisplaySettings className="service-list-icon" />}
            loading={serviceList.loading}
        >
            { serviceList.error && <Error error={serviceList.error} /> }
            { 
                serviceList.services.length > 0 && serviceList.services.map(service =>
                    <Service key={service.name} service={service} />)
            }
        </NavBar>
    );
};

export default ServiceList;