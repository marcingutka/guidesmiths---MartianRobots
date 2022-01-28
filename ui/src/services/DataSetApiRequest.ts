import axios, { Axios, AxiosPromise, AxiosResponse } from 'axios';
import {IDataSet} from '../components/Model/IDataSet'

const dataSetApiUrl = "https://localhost:7236/api/dataSet";

export async function GetDataSets(): Promise<AxiosPromise<IDataSet[]>> {
    const res = await axios.get(dataSetApiUrl); 
    return res;
}

export async function UploadFile(file: File): Promise<AxiosPromise<any>> {
    var formData = new FormData();
    formData.append('file', file);
    return axios.post(dataSetApiUrl + "/upload", formData, {headers: {'Conetnt-Type': 'multipart/form-data'}});
}



