import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router';
import { HomePage } from './components/HomePage/HomePage.tsx';
import { Dictionary } from './components/Dictionary/Dictionary.tsx';
import { AddWord } from './components/AddWord/AddWord.tsx';
import { EditWord } from './components/EditWord/EditWord.tsx';
import { CheckKnowledge } from './components/CheckKnowledge/CheckKnowledge.tsx';
import { Result } from './components/Result/Result.tsx';

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={HomePage}></Route>
          <Route path="/dictionary" Component={Dictionary} />
          <Route path="/new-word" Component={AddWord} />
          <Route path="/edit-word" Component={EditWord} />
          <Route path="/check" Component={CheckKnowledge} />
          <Route path="/result" Component={Result} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
