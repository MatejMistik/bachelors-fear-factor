# AI-FearFactor-InGames Version 1.0
Bachelor Gameplay demo with use of advanced interaction with AI

Slovak easy introduction to subject of this app:

Keďže práca ma byť inovatívna zamýšlal som, ktorá herná mechanika ešte nie je zobrazená. Prišiel s návrhom vyzobrazenia strachu hernej umelel inteligencii. A dôvod? Všetky hry spôsobujú strach iba hráčovi. Ostatné hry využivájú iba minimálne vyjadrenie strachu nepriateľov, ktoré je väčšinou iba riadené skriptom.
Táto práca sa zamerala na to, aby bol strach Umelej Inteligencii reaktívny a aj ho názorne vyzobrazovala. Podarilo sa to predstaviť na 4 rôznych scenároch, kde UI reáguje na hráčove podnety a tým pádom vzniká samostatný simulačný model, ktorý ešte poskytuje nastavenie vstupných hodnôt, ovplyvňujúce tvorbu strachu a tým pádom aj chovania UI. Následne je možné využiť prvky simulácie na vytvorenie hry, čo umožnuje z jedného samostatného celku využiť veľký potenciál znovupoužiteľnosti a zrovna znovupoužiteľnost a modularitu podporuje použitie stromov chovania v práci. Každá scéna má svoj špecifický strom chovania, pre čo najlepšie vyzobrazenie daného scenáru.


Abstract of thesis >

The goal of this thesis is to present the fear factor by gaming artificial intelligence. The work focuses on the player's interaction with artificial intelligence, whose fear factor is addressed by evaluating complex conditions and the subsequent selection of the state of behaviour. The created system works for combat and escape of artificial intelligence. The outcome of this thesis is the implementation of human emotion, mainly the fear for gaming artificial intelligence in the environment of Unity engine.

Thesis: https://wis.fit.vutbr.cz/FIT/st/rp.php/rp/2021/BP/24601.pdf

# Version 1.0 >

Offers 4 scenes displaying fear of AI and the simulation part. And one scene focused on gameplay module of the game it self.

Scenes conduct of predifined reactions which are dynamilly calculated to give the best response of the AI.

Video, showcasing work, is avaible at: https://www.youtube.com/watch?v=6GI_t068qR4&ab_channel=MATEJMI%C5%A0T%C3%8DK.

Scripts avaible in Assets/1.0.Simulation/Scripts

# Future Plans >

1. RayCast pointing will trigger AI reaction, which concludes of pointing it's arms up, as it illustrates giving up.
2. AI will be equipped with hearing sense, which creates room for reacting to gunshots or any sound, that can make AI "feel" fear.
3. Showcasing physical response of hormnones as cortisol and adreanaline, which make your heart go a lot faster, your muscles are prepared for a lot of stress and many others. More in chapter 2 section 4 reaction of body to fear. 
4. Shaking - Aim accuracy of the AI would be decreased by the amount of fear
5. AI field of view -view angle range can differ based on the amount of fear.
