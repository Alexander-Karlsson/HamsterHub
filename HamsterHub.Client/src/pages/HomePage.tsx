import { useEffect, useState } from 'react';
import type { HamsterDto } from '../types/types';
import { getAllHamsters } from '../api/hamsterApi';
import { hamsterImages } from '../assets/hamsters';
import { useNavigate } from 'react-router-dom'
import '../App.css';

function HomePage() {
  const [hamsters, setHamsters] = useState<HamsterDto[]>([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    getAllHamsters()
      .then(setHamsters)
      .finally(() => setLoading(false));
  }, []);

  return (
    <div className="app">
      <header className="hero">
        <div className="hero-inner">
          <p className="eyebrow">Sveriges Största hamstermäklare 2025</p>
          <h1 className="logo">HamsterHub</h1>
          <p className="tagline">Hyr den ultimata hamstern snabbt, enkelt och på dina villkor.</p>
          <button className="cta">Utforska våra gnagare</button>
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
    </div>
  );
}

export default HomePage;