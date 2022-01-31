import axios, { AxiosPromise  } from 'axios';
import { IRobotStep } from '../components/Model/IRobotStep';

const dataSetApiUrl = "https://localhost:7236/api/robots/";

export async function GetRobotsResults(runId: string): Promise<AxiosPromise<Blob>> {
    const res = await axios.get(dataSetApiUrl + runId + "/results/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}

export async function GetRobotsByRunId(runId: string): Promise<AxiosPromise<number>> {
    const res = await axios.get(dataSetApiUrl + runId); 
    return res;
}

export async function GetRobotByRunIdRobotId(runId: string, robotId: string): Promise<AxiosPromise<IRobotStep[]>> {
    const res = await axios.get(dataSetApiUrl + runId + "/robot/" + robotId); 
    return res;
}