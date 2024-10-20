import { FC, useEffect } from 'react';
import { PendingActions } from '@mui/icons-material';
import { useAppDispatch, useAppSelector } from '../../redux/hooks';
import { RootState } from '../../redux/store';
import { update, updateScheduleRange, remove, setError, setLoading, authorized } from './ScheduleListStore';
import { useConnection } from '../../hooks';
import { api } from '../../utils/api';
import { unauthorized } from '../../App/AppStore';
import Schedule from '../Schedule/Schedule';
import NavBar from '../NavBar/NavBar';
import Error from '../../components/Error/Error';
import './ScheduleList.scss';

const ScheduleList: FC = () => {
    const dispatch = useAppDispatch();
    const scheduleList = useAppSelector(({ scheduleList }: RootState) => scheduleList);

    const connection = useConnection('/hub/scheduler', {
        invoke: () => {
            dispatch(setLoading(true));
            return ['Schedules', []];
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
            ['Schedule', schedule => dispatch(update(schedule))],
            ['Schedules', schedules => dispatch(updateScheduleRange(schedules))],
            ['RemoveSchedule', schedule => dispatch(remove(schedule))]
        ]
    });

    useEffect(() => {
        connection.start();
        return () => {
            connection.stop();
        };
    }, []);

    useEffect(() => {
        if (!scheduleList.auth) {
            dispatch(authorized());
            dispatch(unauthorized());
        }
    }, [scheduleList.auth]);

    return (
        <NavBar
            title="Scheduler"
            icon={<PendingActions className="schedule-list-icon" />}
            loading={scheduleList.loading}
        >
            { scheduleList.error && <Error error={scheduleList.error} /> }
            { 
                scheduleList.schedules.length > 0 && scheduleList.schedules.map(schedule =>
                    <Schedule key={schedule.name} schedule={schedule} />)
            }
        </NavBar>
    );
};

export default ScheduleList;