import { useState, FC } from 'react';
import { Accordion, CircularProgress, TextField } from '@mui/material';
import { Alarm, AlarmOff } from '@mui/icons-material';
import { useAppDispatch } from '../../redux/hooks';
import { updateSchedule } from '../ScheduleList/ScheduleListStore';
import { ScheduleInfo } from '../ScheduleList/data';
import { AccordionBody, AccordionHeader, Text } from '../../components';
import { Parameters } from '../ParameterList/data';
import ParameterList from '../ParameterList/ParameterList';
import ParameterHeader from '../../components/ParameterHeader/ParameterHeader';
import './Schedule.scss';

interface ScheduleProps {
    schedule: ScheduleInfo;
}

const Schedule: FC<Readonly<ScheduleProps>> = ({ schedule }) => {
    const dispatch = useAppDispatch();
    const [expanded, setExpanded] = useState(false);
    const [modifiedParameters, setModifiedParameters] = useState(schedule.parameters);
    
    const onSaveClick = async (parameters: Parameters) => {
        await dispatch(updateSchedule({
            name: schedule.name,
            parameters: parameters
        }));
    };
    
    return (
        <Accordion
            expanded={expanded} 
            onChange={(_e, s) => setExpanded(s)}
        >
            <AccordionHeader>
                <div className="schedule-header-container">
                    {
                        schedule.loading
                            ? <CircularProgress className="schedule-progress" />
                            : (schedule.error
                                ? <AlarmOff className="schedule-fail-icon" />
                                : (schedule.isRunning
                                    ? <Alarm className="schedule-success-icon" />
                                    : <AlarmOff className="schedule-success-icon" />)
                            )
                    }
                    <div>
                        <div className="schedule-header-container schedule-name">
                            <Text>{schedule.name}</Text>
                        </div>
                        {
                            schedule.description &&
                                <Text assignment="description" multiline>{ schedule.description }</Text>
                        }
                    </div>
                </div>
            </AccordionHeader>
            <AccordionBody indent>
                {
                    schedule.updateError &&
                        <Text assignment="error" multiline>{ schedule.updateError }</Text>
                }
                <ParameterList
                    parameters={schedule.parameters}
                    modifiedParameters={modifiedParameters}
                    disable={schedule.loading}
                    error={schedule.error}
                    stackTrace={schedule.stackTrace}
                    setModifiedParameters={setModifiedParameters}
                    onSave={onSaveClick}
                />
                <div>
                    <ParameterHeader name="Last Success" />
                    <TextField
                        value={schedule.lastSuccess ?? ''}
                        disabled={true}
                        fullWidth
                        multiline
                    />
                </div>
            </AccordionBody>
        </Accordion>
    );
};

export default Schedule;