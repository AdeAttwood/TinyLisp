;; Testing some the maths functions

(deftest it-will-add-numbers-together ()
  (assert-eq 2 (+ 1 1)))

(deftest it-will-subtract-numbers-together ()
  (assert-eq 2 (- 4 2)))

(deftest it-will-divide-numbers-together ()
  (assert-eq 2 (/ 4 2)))

(deftest it-will-times-numbers-together ()
  (assert-eq 8 (* 4 2)))
