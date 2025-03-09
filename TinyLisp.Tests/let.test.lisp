(deftest it-will-do-a-let-binding ()
  ;; We have a binding to a and b. The binding to b includes a reference to a
  ;; that is defined previously in the let block.
  (let ((a 1) (b (+ a 1)))
    (assert-eq a 1)
    (assert-eq b 2)

    ;; Nested bindings will override the previously defined b
    (let ((b 10))
      (assert-eq b 10))

    ;; When the nested let is done the previous value is restored
    (assert-eq b 2)))

(deftest it-returns-the-last-value-from-let ()
  (defun run ()
    (let ((a 10))
      (let ((b 20))
        (+ a b))))

  (assert-eq 30 (run)))
