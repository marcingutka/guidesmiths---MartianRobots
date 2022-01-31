import React from "react";
import { useParams } from "react-router";

export const RobotSummary = () =>
{
    const {id, robotId} = useParams();

    return (
        <React.Fragment>
            {id} {robotId}
        </React.Fragment>
    )
}