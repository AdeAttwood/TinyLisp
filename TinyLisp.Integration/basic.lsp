;; The really basic tests first tests for tiny lisp

;; Test we can do some maths
(assert (= (+ 1 1) 2) "One plus one must be 2")
(assert (= (- 1 1) 0) "One minus one must be 0")
(assert (= (/ 10 2) 5) "This must be 5")

;; Run and call a function
(defun app/plus-one (number)
  (+ number 1))

(assert (= 2 (app/plus-one 1)) "This should be 2")
