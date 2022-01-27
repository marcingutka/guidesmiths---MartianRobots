import React from 'react';
import {BrowserRouter,  Routes, Route } from "react-router-dom";
import { Main } from './components/Main'
import './App.css';

function App() {
  return (
    <React.Fragment>
        <BrowserRouter>
        <Routes>
          <Route path="/" element={<Main />}></Route>
        </Routes>
        </BrowserRouter>
    </React.Fragment>
  );
}

export default App;
