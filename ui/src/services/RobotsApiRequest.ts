import axios, { AxiosPromise  } from 'axios';

const dataSetApiUrl = "https://localhost:7236/api/robots/";

export async function GetRobotsResults(runId: string): Promise<AxiosPromise<Blob>> {
    const res = await axios.get(dataSetApiUrl + runId + "/results/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}

export async function GetRobotsByRunId(runId: string): Promise<AxiosPromise<number>> {
    const res = await axios.get(dataSetApiUrl + runId); 
    return res;
}

export async function GetRobotByRunIdRobotId(runId: string, robotId: string): Promise<AxiosPromise<number>> {
    const res = await axios.get(dataSetApiUrl + runId + "/robot/" + robotId); 
    return res;
}