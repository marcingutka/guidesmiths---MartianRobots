import axios, { AxiosPromise  } from 'axios';
import { basicUrl } from "./Common";
import { IRobotStep } from '../components/Model/IRobotStep';

const baseApiUrl = basicUrl + "robots/";

export async function GetRobotsByRunId(runId: string): Promise<AxiosPromise<number>> {
    const res = await axios.get(baseApiUrl + runId); 
    return res;
}

export async function GetRobotByRunIdRobotId(runId: string, robotId: string): Promise<AxiosPromise<IRobotStep[]>> {
    const res = await axios.get(baseApiUrl + runId + "/robot/" + robotId); 
    return res;
}