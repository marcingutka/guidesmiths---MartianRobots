import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { IDataSet } from '../components/Model/IDataSet'
import { Position } from '../components/Model/IPosition'

const baseApiUrl = basicUrl + "dataSet/";

export async function GetDataSets(): Promise<AxiosPromise<IDataSet[]>> {
    const res = await axios.get(baseApiUrl); 
    return res;
}

export async function GetGridByRunId(runId: string): Promise<AxiosPromise<Position>> {
    const res = await axios.get(baseApiUrl + "grid/" + runId); 
    return res;
}

export async function UploadFile(file: File, runName: string): Promise<AxiosPromise<any>> {
    var name = runName.length === 0? file.name : runName;
    console.log(runName);
    var formData = new FormData();
    formData.append('file', file);
    return await axios.post(baseApiUrl + "upload/" + name, formData, {headers: {'Content-Type': 'multipart/form-data'}});
}

export async function DeleteDataSet(runId: string): Promise<AxiosPromise<any>> {    
    return await axios.delete(baseApiUrl + runId, {headers: {'Access-Control-Allow-Origin': '*'}});
}

export async function GetRobotsResults(runId: string): Promise<AxiosPromise<Blob>> {
    const res = await axios.get(baseApiUrl + "results/" + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}}); 
    return res;
}