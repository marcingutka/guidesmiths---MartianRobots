import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { DataSet } from '../components/Model/IDataSet'
import { Position } from '../components/Model/IPosition'

const baseApiUrl = basicUrl + "dataSet/";

export async function getDataSets(): Promise<AxiosPromise<DataSet[]>> {
    const res = await axios.get(baseApiUrl); 
    return res;
}

export async function getGridByRunId(runId: string): Promise<AxiosPromise<Position>> {
    const res = await axios.get(baseApiUrl + "grid/" + runId); 
    return res;
}

export function uploadFile(file: File, runName: string): AxiosPromise<any> {
    let name = runName.length === 0? file.name : runName;
    let formData = new FormData();
    formData.append('file', file);
    return axios.post(baseApiUrl + "upload/" + name, formData, {headers: {'Content-Type': 'multipart/form-data'}});
}

export async function deleteDataSet(runId: string): Promise<AxiosPromise<any>> {    
    return await axios.delete(baseApiUrl + runId);
}

export async function getRunResults(runId: string): Promise<AxiosPromise<Blob>> {
    const res = axios.get(baseApiUrl + "results/" + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}

export async function getRunInput(runId: string): Promise<AxiosPromise<Blob>> {
    const res = await axios.get(baseApiUrl + "input/" + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}