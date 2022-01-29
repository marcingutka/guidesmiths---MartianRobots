import axios, { Axios, AxiosPromise, AxiosResponse } from 'axios';
import { IDataSet } from '../components/Model/IDataSet'

const dataSetApiUrl = "https://localhost:7236/api/robots/";

export async function GetRobotsResults(runId: string): Promise<AxiosPromise<Blob>> {
    const res = await axios.get(dataSetApiUrl + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}