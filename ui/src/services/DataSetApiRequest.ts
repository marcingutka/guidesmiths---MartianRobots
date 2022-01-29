import axios, { Axios, AxiosPromise, AxiosResponse } from 'axios';
import {IDataSet} from '../components/Model/IDataSet'

const dataSetApiUrl = "https://localhost:7236/api/dataSet";

export async function GetDataSets(): Promise<AxiosPromise<IDataSet[]>> {
    const res = await axios.get(dataSetApiUrl); 
    return res;
}

export async function UploadFile(file: File, runName: string): Promise<AxiosPromise<any>> {
    var name = runName.length === 0? file.name : runName;
    console.log(runName);
    var formData = new FormData();
    formData.append('file', file);
    return await axios.post(dataSetApiUrl + "/upload/" + name, formData, {headers: {'Conetnt-Type': 'multipart/form-data'}});
}

export async function DeleteDataSet(runId: string): Promise<AxiosPromise<any>> {    
    return await axios.delete(dataSetApiUrl + "/" + runId, {headers: {'Access-Control-Allow-Origin': '*'}});
}



