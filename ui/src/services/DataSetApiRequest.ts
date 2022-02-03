import axios, {  AxiosPromise } from 'axios';
import { basicUrl } from "./Common";
import { DataSet } from '../components/Model/DataSet'
import { Position } from '../components/Model/Position'

const baseApiUrl = basicUrl + "dataSet/";

export function getDataSets(): AxiosPromise<DataSet[]> {
    return axios.get(baseApiUrl); 
}

export function getGridByRunId(runId: string): AxiosPromise<Position> {
    return axios.get(baseApiUrl + "grid/" + runId); 
}

export function uploadFile(file: File, runName: string): AxiosPromise<any> {
    let name = runName.length === 0? file.name : runName;
    let formData = new FormData();
    formData.append('file', file);
    return axios.post(baseApiUrl + "upload/" + name, formData, {headers: {'Content-Type': 'multipart/form-data'}});
}

export function deleteDataSet(runId: string): AxiosPromise<any> {    
    return axios.delete(baseApiUrl + runId);
}

export function getRunResults(runId: string): AxiosPromise<Blob> {
    return axios.get(baseApiUrl + "results/" + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}});
}

export function getRunInput(runId: string): AxiosPromise<Blob> {
    return axios.get(baseApiUrl + "input/" + runId + "/download", {headers: {"Content-Type": "application/octet-stream"}});
}