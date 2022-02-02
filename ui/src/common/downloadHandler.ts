import FileDownload  from "js-file-download";
import { getRunResults, getRunInput } from "../services/DataSetApiRequest";

export const resultsDownloadHandler = async (runId: string) => {
    const response = await getRunResults(runId);
    const fileNameAnnex = " - Result.txt"
    const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
    FileDownload(response.data, fileName? fileName.replace(".txt","") + fileNameAnnex : {runId} + fileNameAnnex);
}

export const inputDownloadHandler = async (runId: string) => {
    const response = await getRunInput(runId);
    const fileNameAnnex = " - Input.txt"
    const fileName = response.headers['content-disposition'].split('filename=')[1].split(';')[0];
    FileDownload(response.data, fileName? fileName.replace(".txt","") + fileNameAnnex : {runId} + fileNameAnnex);
}