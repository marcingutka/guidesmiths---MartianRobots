import React from 'react';
import {BrowserRouter,  Routes, Route } from "react-router-dom";
import { Main } from './components/Main'
import { RunSummary } from './components/RunSummary';
import './App.css';

function App() {
  return (
    <React.Fragment>
        <BrowserRouter>
        <Routes>
          <Route path="/" element={<Main />}/>
          <Route path="/run/:id" element={<RunSummary />}/>
        </Routes>
        </BrowserRouter>
    </React.Fragment>
  );
}

export default App;
