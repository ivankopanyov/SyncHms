import { useState, FC, useEffect } from 'react';
import { Accordion } from '@mui/material';
import { CreditCardOutlined, CreditCardOffOutlined, Person, PersonOff,
    Sync, SyncDisabled, Lock, NoEncryption, Done, DoneOutline } from '@mui/icons-material'
import { TaskInfo } from '../LogList/data';
import { getTask } from '../LogList/LogListStore';
import { AccordionBody, AccordionHeader, Text, Loading } from '../../components';
import { useAppDispatch } from '../../redux/hooks';
import { useLoopRequest } from '../../hooks';
import { dateDisplay } from '../../utils/mapper';
import Log from '../LogInfo/Log';
import './Task.scss';
 
interface TaskProps {
    task: TaskInfo;
}

const Task: FC<Readonly<TaskProps>> = ({ task }) => {
    const failed = task.logs[0].isEnd && task.logs[0].isError;
    const style = task.logs[0].isError ? 'task-icon-fail'
        : (task.logs[0].isEnd ? 'task-icon-success' : 'task-icon');

    const dispatch = useAppDispatch();
    const [expanded, setExpanded] = useState<boolean>(false);
    const loopRequest = useLoopRequest(async () => {
        const result = await dispatch(getTask(task.logs[0].taskId));
        return result.meta.requestStatus === 'fulfilled';
    });

    useEffect(() => {
        if (expanded && task.loading !== false) {
            loopRequest.start();
        } else if (!expanded) {
            loopRequest.stop();
        }
    }, [expanded]);

    useEffect(() => {
        return(() => {
            loopRequest.stop();
        });
    }, []);
    
    return (
        <Accordion expanded={expanded} onChange={(_e, s) => setExpanded(s)}>
            <AccordionHeader>
                <div className="task-header">
                    <div className="task-indent-right">
                        {
                            {
                                'RESV': failed
                                    ? <PersonOff className={style} />
                                    : <Person className={style} />,
                                'POST': failed
                                    ? <CreditCardOffOutlined className={style} />
                                    : <CreditCardOutlined className={style} />,
                                'LOCK': failed
                                    ? <NoEncryption className={style} />
                                    : <Lock className={style} />
                            } [task.logs[0].taskName ?? ''] || (failed
                                ? <SyncDisabled className={style} />
                                : <Sync className={style} />)
                        }
                        {
                            task.logs[0].isEnd && !task.logs[0].isError && 
                                <div>
                                    <DoneOutline className="task-icon-outlined task-icon-done" />
                                    <Done className="task-icon-success task-icon-done" />
                                </div>
                        }
                    </div>
                    <div>
                        <div className="task-header-name">
                            <Text>{ dateDisplay(task.logs[0].dateTime) }</Text>
                            {
                                !expanded && task.logs[0].handlerName &&
                                    <div className="task-handler task-indent-left">
                                        <Text>{ task.logs[0].handlerName }</Text>
                                    </div> 
                            }
                        </div>
                        {
                            !expanded && task.logs[0].message &&
                                <Text assignment="description" multiline>{ task.logs[0].message }</Text>
                        }
                        {
                            expanded && task.error &&
                                <Text assignment="error" multiline>{ task.error }</Text>
                        }
                    </div>
                </div>
            </AccordionHeader>
            <AccordionBody indent>
                { task.logs.map((log, index) => <Log key={index} log={log} />) }
                { (task.loading === undefined || task.loading) && <Loading /> }
            </AccordionBody>
        </Accordion>
    );
};

export default Task;