import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router';
import { HomePage } from './components/HomePage/HomePage.tsx';
import { DictionaryPage } from './components/DictionaryPage/DictionaryPage.tsx';
import { AddWordPage } from './components/AddWordPage/AddWordPage.tsx';
import { EditWordPage } from './components/EditWordPage/EditWordPage.tsx';
import { CheckKnowledgePage } from './components/CheckKnowledgePage/CheckKnowledgePage.tsx';
import { ResultPage } from './components/ResultPage/ResultPage.tsx';
import { WordsProvider } from './WordsProvider.tsx';

function App() {
  return (
    <>
      <WordsProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/dictionary" element={<DictionaryPage />} />
            <Route path="/new-word" element={<AddWordPage />} />
            <Route path="/edit-word/:id" element={<EditWordPage />} />
            <Route path="/check" element={<CheckKnowledgePage />} />
            <Route path="/result" element={<ResultPage />} />
          </Routes>
        </BrowserRouter>
      </WordsProvider>
    </>
  );
}

export default App;
