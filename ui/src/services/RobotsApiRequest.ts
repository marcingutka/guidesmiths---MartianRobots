import axios, { AxiosPromise  } from 'axios';
import { basicUrl } from "./Common";
import { RobotStep } from '../components/Model/IRobotStep';

const baseApiUrl = basicUrl + "robots/";

export function getRobotsByRunId(runId: string): AxiosPromise<number> {
    return axios.get(baseApiUrl + runId);
}

export function getRobotByRunIdRobotId(runId: string, robotId: string): AxiosPromise<RobotStep[]> {
    return axios.get(baseApiUrl + runId + "/robot/" + robotId);
}