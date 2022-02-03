import axios, { AxiosPromise  } from 'axios';
import { basicUrl } from "./Common";
import { RobotStep } from '../components/Model/IRobotStep';

const baseApiUrl = basicUrl + "robots/";

export async function getRobotsByRunId(runId: string): Promise<AxiosPromise<number>> {
    const res = await axios.get(baseApiUrl + runId); 
    return res;
}

export async function getRobotByRunIdRobotId(runId: string, robotId: string): Promise<AxiosPromise<RobotStep[]>> {
    const res = await axios.get(baseApiUrl + runId + "/robot/" + robotId); 
    return res;
}