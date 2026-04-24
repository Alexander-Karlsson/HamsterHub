import { useEffect, useState } from 'react';
import type { HamsterDto } from '../types/types';
import { getAllHamsters } from '../api/hamsterApi';
import { hamsterImages } from '../assets/hamsters';
import { useNavigate } from 'react-router-dom';
import NewsLetterModal from '../components/NewsLetterModal';
import StarRating from '../components/StarRating';
import '../App.css';

function HomePage() {
  const [hamsters, setHamsters] = useState<HamsterDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [showNewsletter, setShowNewsletter] = useState(false);
  const navigate = useNavigate();
  
  useEffect(() => {
    getAllHamsters()
      .then(setHamsters)
      .finally(() => setLoading(false));
  }, []);

    useEffect(() => {
    const dismissed = localStorage.getItem('newsletter_dismissed');
    if (!dismissed) {
      const timer = setTimeout(() => setShowNewsletter(true), 1200);
      return () => clearTimeout(timer);
    }
  }, []);

  const handleClose = () => {
    localStorage.setItem('newsletter_dismissed', 'true');
    setShowNewsletter(false);
  };

  return (
    <div className="app">
      <header className="hero">
        <div className="hero-inner">
          <p className="eyebrow">⭐️ Sveriges största hamstermäklare 2025 ⭐️</p>
          <h1 className="logo">HAMSTERHUB</h1>
          <p className="tagline">Hyr din ultimata hamster snabbt, enkelt och på dina villkor.</p>
          <button className="cta">Utforska oss 🐹</button>
        </div>
      </header>

      <main className="catalog">
        <div className="catalog-header">
          <h2>Tillgängliga gnagare</h2>
          <span className="count">{hamsters.length} st</span>
        </div>

        {loading ? (
          <p className="loading">Hämtar hamstrar...</p>
        ) : (
          <div className="grid">
            {hamsters.map((h, index)=> (
              <div className="card" key={h.id} style={{ animationDelay: `${index * 0.12}s`}}>
                <div className="card-avatar">
                  {hamsterImages[h.id] ? (
                  <img
                    src={hamsterImages[h.id]}
                    alt={h.name}
                    style={{ width: '100%', height: '100%', objectFit: 'cover' }}
                  />
                ) : (
                  h.name[0]
                )}  
                </div>
                <div className="card-body">
                  <h3>{h.name}</h3> 
                  <span className="personality">{h.personality}</span>
                  <StarRating score={h.averageScore} />
                  <p className="description">{h.weightInGrams}g</p>   
                  <p className="description">{h.description}</p>
                </div>
                <div className="card-footer">
                  <span className="price">{h.pricePerDay} kr<small>/dag</small></span>
                  <button className="book-btn" onClick={() => navigate(`/book/${h.id}`)}>
                    Boka
                    </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </main> 

      {showNewsletter && <NewsLetterModal onClose={handleClose} />}
    </div>
  );
}

export default HomePage;